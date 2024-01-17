namespace Services.FamilyMembers;

using Domain;
using Domain.Finances;
using Services.Exceptions;

public class WrongFamilyMemberOwnerException(int[] familyMemberIds, int ownerId) : ServiceException(
    $"{nameof(User)} with {nameof(User.Id)} = {ownerId} does not own some of {nameof(Bank)}s with " +
    $"{nameof(FamilyMember.Id)} in [{string.Join(", ", familyMemberIds)}]")
{
    public int[] FamilyMemberIds { get; } = familyMemberIds;
    public int OwnerId { get; } = ownerId;
}