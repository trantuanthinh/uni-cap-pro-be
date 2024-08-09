namespace uni_cap_pro_be.Utils
{
    public class API_ResponseConvention
    {
        public object ResponseMessage(string _message, object _item)
        {
            return new
            {
                message = _message,
                ok = true,
                data = _item
            };
        }
    }
}
