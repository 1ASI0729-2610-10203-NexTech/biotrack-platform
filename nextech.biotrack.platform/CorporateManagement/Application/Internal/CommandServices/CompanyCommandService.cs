using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.CorporateManagement.Application.CommandServices;
using nextech.biotrack.platform.CorporateManagement.Domain.Model;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Commands;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.ValueObjects;
using nextech.biotrack.platform.CorporateManagement.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.CorporateManagement.Application.Internal.CommandServices;

public class CompanyCommandService(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICompanyCommandService
{
    public async Task<Result<Company>> Handle(RegisterCompanyCommand command, CancellationToken cancellationToken)
    {
        if (!Ruc.IsValid(command.Ruc))
            return Result<Company>.Failure(CorporateManagementError.InvalidRucFormat,
                "The RUC must be exactly 11 digits.");

        if (await companyRepository.ExistsByRucAsync(command.Ruc, cancellationToken))
            return Result<Company>.Failure(CorporateManagementError.RucAlreadyTaken,
                $"A company with RUC '{command.Ruc}' is already registered.");

        var company = new Company(
            command.Name, command.Ruc, command.Sector,
            command.Country, command.City, command.OwnerId);

        try
        {
            await companyRepository.AddAsync(company, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Company>.Success(company);
        }
        catch (OperationCanceledException)
        {
            return Result<Company>.Failure(CorporateManagementError.OperationCancelled,
                "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Company>.Failure(CorporateManagementError.DatabaseError,
                "A database error occurred while registering the company.");
        }
        catch (Exception)
        {
            return Result<Company>.Failure(CorporateManagementError.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    public async Task<Result> Handle(UploadCompanyCollaboratorsCommand command, CancellationToken cancellationToken)
    {
        var company = await companyRepository.FindByIdWithCollaboratorsAsync(command.CompanyId, cancellationToken);

        if (company == null)
            return Result.Failure(CorporateManagementError.CompanyNotFound,
                $"Company with ID {command.CompanyId} was not found.");

        if (company.OwnerId != command.RequestingUserId)
            return Result.Failure(CorporateManagementError.AccessDenied,
                "You do not have access to this company.");

        var collaboratorList = command.Collaborators.ToList();

        if (collaboratorList.Any(c =>
                string.IsNullOrWhiteSpace(c.FirstName) ||
                string.IsNullOrWhiteSpace(c.LastName) ||
                string.IsNullOrWhiteSpace(c.Email) ||
                string.IsNullOrWhiteSpace(c.DocumentNumber)))
            return Result.Failure(CorporateManagementError.InvalidCollaboratorData,
                "One or more collaborators have missing required fields.");

        company.ReplaceCollaborators(
            collaboratorList.Select(c => (c.FirstName, c.LastName, c.Email, c.DocumentNumber)));

        companyRepository.Update(company);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(CorporateManagementError.DatabaseError,
                "A database error occurred while uploading collaborators.");
        }
    }
}
