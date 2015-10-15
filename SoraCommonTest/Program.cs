using NUnit.Framework;
using SoraCommonNet;
using System.Threading.Tasks;

namespace SoraCommonTest
{
    public class Performer
    {
        readonly string account0Email;
        readonly string account0Password;
        readonly string account0VerificationCode;
        readonly string account1Email;
        readonly string account1Password;
        readonly string account1FirstSubscriberImsi;

        public Performer()
        {
            account0Email = Properties.Settings.Default.ACCOUNT0_EMAIL;
            account0Password = Properties.Settings.Default.ACCOUNT0_PASSWORD;
            account0VerificationCode = Properties.Settings.Default.ACCOUNT0_VERIFICATION_CODE;
            account1Email = Properties.Settings.Default.ACCOUNT1_EMAIL;
            account1Password = Properties.Settings.Default.ACCOUNT1_PASSWORD;
            account1FirstSubscriberImsi = Properties.Settings.Default.ACCOUNT1_FIRST_SUBSCRIBER_IMSI;
        }

        public async Task Perform()
        {
            /*
            // Assert.IsTrue(await Operator.Register(account0Email, account0Password));
            // Assert.IsTrue(await Operator.VerifyRegistration(account0VerificationCode));
            var op = await Operator.Authenticate(account0Email, account0Password);
            Assert.IsNotNull(op);
            var opInfo = await op.FetchInfo();
            Assert.IsNotNull(opInfo);
            Assert.IsNotNullOrEmpty(opInfo.CreateDate);
            Assert.IsNullOrEmpty(opInfo.Description);
            Assert.IsNotNullOrEmpty(opInfo.Email);
            Assert.IsNotNullOrEmpty(opInfo.OperatorId);
            Assert.IsNullOrEmpty(opInfo.RootOperatorId);
            Assert.IsNotNullOrEmpty(opInfo.UpdateDate);
            Assert.IsTrue(await op.RefreshToken());
            Assert.IsNotNull(await op.FetchInfo());
            Assert.IsNotNullOrEmpty(await op.FetchSupportToken());
            */
            var op = await Operator.Authenticate(account1Email, account1Password);
            var subscr = await op.RetrieveSubscriber(account1FirstSubscriberImsi);
            Assert.IsNotNull(subscr);
            Assert.AreEqual(account1FirstSubscriberImsi, subscr.Imsi);
            /*
            var subscribers = await op.ListSubscribers();
            Assert.IsNotNull(subscribers);
            Assert.AreNotEqual(0, subscribers.Count);
            Assert.AreEqual(subscr.Imsi, subscribers[0].Imsi);
            */
            // Assert.IsTrue(await subscr.EnableTermination());
            // Assert.IsTrue(await subscr.Activate());
            // Assert.IsTrue(await subscr.Deactivate());
            // Assert.IsTrue(await subscr.Terminate());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var performer = new Performer();
            Task.Run(() => performer.Perform()).Wait();
        }
    }
}
