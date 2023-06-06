namespace NeZoviReg.Domain.Model.Auth.Enum;

public enum RoleType
{
    None = 0,
    
    //RATEL or partner
    Admin = 1,

    //Content provder
    Trgovac,

    //Operater
    Obveznik,

    //End user (from RATEL site), only check its own number
    Potrosac,
}