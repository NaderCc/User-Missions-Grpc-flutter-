using Grpc.Core;

namespace NewMission.Services
{
    public interface IPrinter
    {
        public void UserLoginPrinterRequest();
        public void UserLogoutPrinterReply(string name);

        public void InputParameter(string parameter);

    }
}
    