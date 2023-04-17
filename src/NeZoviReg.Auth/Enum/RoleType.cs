namespace NeZoviReg.Auth.Enum;

public enum RoleType
{
    //RATEL or partner
    Admin = 1,

    //Content provder
    Trgovac,

    //Operater
    Obveznik,

    //End user (from RATEL site), only check its own number
    Potrosac,
}