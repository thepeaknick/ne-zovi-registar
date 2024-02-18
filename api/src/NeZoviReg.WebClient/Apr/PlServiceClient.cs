namespace NeZoviReg.WebClient.PlService;

public partial class PlServiceClient: IDisposable
{
    private bool _isDisposed;

    public void Dispose()
    {
        WebClientUtilities.Dispose(this, ref _isDisposed);
    }

    ~PlServiceClient()
    {
        Dispose();
    }
}