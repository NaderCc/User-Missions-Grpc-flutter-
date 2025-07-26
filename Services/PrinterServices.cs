using Grpc.Core;

namespace NewMission.Services
{
    public class PrinterServices : IPrinter
    {
        public void InputParameter(string parameter_name)
        {
            Console.WriteLine($"Please Enter a value for {parameter_name}:");
            
        }

        public void UserLoginPrinterRequest()
        {
            Console.WriteLine("the Email or Password isn't correct please try again");
            throw new RpcException(new(StatusCode.InvalidArgument, "Enter correct argument"));
        }

        public void UserLogoutPrinterReply(string name)
        {
            Console.WriteLine($"Login successful!!\nWelcomeback Mr:{name}");

        }
    }
}


