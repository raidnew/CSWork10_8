namespace Common;
public enum Role { Manager, Consultant }

public class RoleAccess
{
    public static bool CanCreate(Role role)
    {
        return role == Role.Manager;
    }
}