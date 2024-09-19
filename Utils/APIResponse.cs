namespace uni_cap_pro_be.Utils
{
    // DONE
    public class APIResponse
    {
        public object Success(string _message, object _item)
        {
            return new
            {
                message = $"{_message} Successfully",
                ok = true,
                result = _item
            };
        }

        public object Success(string _token, string _message, object _item)
        {
            return new
            {
                token = _token,
                message = $"{_message} Successfully",
                ok = true,
                result = _item
            };
        }

        public object Failure(string _message)
        {
            return new { message = $"{_message} Failed", ok = false, };
        }

        public object Failure(string _message, object _item)
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
