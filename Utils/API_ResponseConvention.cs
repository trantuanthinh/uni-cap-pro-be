namespace uni_cap_pro_be.Utils
{
    public class API_ResponseConvention
    {
        public object OkMessage(string _message, object _item)
        {
            return new
            {
                message = $"{_message} Successfully",
                ok = true,
                data = _item
            };
        }

        public object OkMessage(string _token, string _message, object _item)
        {
            return new
            {
                token = _token,
                message = $"{_message} Successfully",
                ok = true,
                data = _item
            };
        }

        public object FailedMessage(string _message)
        {
            return new { message = $"{_message} Failed", ok = false, };
        }

        public object FailedMessage(string _message, object _item)
        {
            return new
            {
                message = $"{_message} Failed",
                ok = false,
                state = _item
            };
        }
    }
}
