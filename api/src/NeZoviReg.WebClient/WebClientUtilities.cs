using System.ServiceModel;

namespace NeZoviReg.WebClient;

public static class WebClientUtilities
{
    public static void Dispose(ICommunicationObject service, ref bool isDisposed)
    {
        if (isDisposed)
            return;

        try
        {
            if (service.State == CommunicationState.Faulted)
                service.Abort();
            else
            {
                try
                {
                    service.Close();
                }
                catch (Exception closeException)
                {
                    try
                    {
                        service.Abort();
                    }
                    catch (Exception abortException)
                    {
                        throw new AggregateException(closeException, abortException);
                    }
                    throw;
                }
            }
        }
        finally
        {
            isDisposed = true;
        }
    }
}