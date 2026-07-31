using System.IO.Ports;
using System.Globalization;

namespace BakeryPOS.Core.Services.Hardware;

public class ScaleService : IScaleService
{
    private SerialPort? _serialPort;

    public bool IsConnected => _serialPort != null && _serialPort.IsOpen;

    public void Connect(string portName, int baudRate = 9600)
    {
        _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        _serialPort.Open();
    }

    public void Disconnect()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
            _serialPort.Dispose();
        }
    }

    public decimal ReadWeight()
    {
        if (!IsConnected || _serialPort == null) return 0.000m;

        try
        {
            string rawData = _serialPort.ReadLine().Trim();
            // Limpia caracteres no numéricos dejando punto o coma decimal
            string cleanData = new string(rawData.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
            
            if (decimal.TryParse(cleanData, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal weight))
            {
                return weight;
            }
        }
        catch
        {
            // Timeout o lectura errónea de puerto serie
        }

        return 0.000m;
    }
}