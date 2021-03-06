using Meadow.Hardware;

namespace Meadow.Foundation.Displays.ePaper
{
    public class EPD2i9 : EPD1i54
    {
        public EPD2i9(IDigitalPin chipSelectPin, IDigitalPin dcPin, IDigitalPin resetPin, IDigitalPin busyPin,
            Spi.SPI_module spiModule = Spi.SPI_module.SPI1, uint speedKHz = (uint)9500):base(chipSelectPin, dcPin, resetPin, busyPin, spiModule, speedKHz)
        { }

        public override uint Width => 128;
        public override uint Height => 296;
    }
}