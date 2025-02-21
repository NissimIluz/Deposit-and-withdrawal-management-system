using Application.Enums;

namespace Application.Models
{
    public class Response<T> where T : class
    {
        public Response(){ }

        public Response(eCodes code)
        {
            Code = code;
        }

        public Response(T data)
        {
            Data = data;
            Code = eCodes.Secsses;
        }

        public eCodes Code { get; set; }
        public T Data  { get; set; }
    }
}
