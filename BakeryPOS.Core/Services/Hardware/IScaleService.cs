namespace BakeryPOS.Core.Services.Hardware;

public interface IScaleService
{
    void Connect(string portName, int baudRate = 9600);
    void Disconnect();
    decimal ReadWeight();
    bool IsConnected { get; }
}