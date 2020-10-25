using Data.Model;

namespace Data.BLL
{
    public class BaseBLL
    {
        public TBL_USER USER { get { return new Base().USER; } }
    }
}
