﻿using Meadow.Hardware;
using System.Threading;

namespace Meadow.Foundation.Displays.Tft
{
    public class ILI9163 : DisplayTftSpiBase
    {
        private ILI9163() { }

        public ILI9163(IDigitalPin chipSelectPin, IDigitalPin dcPin, IDigitalPin resetPin,
            uint width, uint height,
            Spi.SPI_module spiModule = Spi.SPI_module.SPI1,
            uint speedKHz = 9500) : base(chipSelectPin, dcPin, resetPin, width, height, spiModule, speedKHz)
        {
            Initialize();
        }
        
        protected override void Initialize()
        {
            resetPort.State = (true);
            Thread.Sleep(50);
            resetPort.State = (false);
            Thread.Sleep(50);
            resetPort.State = (true);
            Thread.Sleep(50);

            dataCommandPort.State = (Command); //software reset
            Write(0x01);

            dataCommandPort.State = (Command); //exit sleep mode
            Write(0x11);

            dataCommandPort.State = (Command); //set pixel format
            Write(0x3A);
            dataCommandPort.State = (Data);
            Write(0x05);//16 bit 565

            dataCommandPort.State = (Command);
            Write(0x26);
            dataCommandPort.State = (Data);
            Write(0x04);

            dataCommandPort.State = (Command);
            Write(0x36);
            dataCommandPort.State = (Data);
            Write(0x00); //set to RGB

            dataCommandPort.State = (Command);
            Write(0xF2);
            dataCommandPort.State = (Data);
            Write(0x01);

            dataCommandPort.State = (Command);
            Write(0xE0);
            dataCommandPort.State = (Data);
            Write(0x04);
            Write(0x3F);
            Write(0x25);
            Write(0x1C);
            Write(0x1E);
            Write(0x20);
            Write(0x12);
            Write(0x2A);
            Write(0x90);
            Write(0x24);
            Write(0x11);
            Write(0x00);
            Write(0x00);
            Write(0x00);
            Write(0x00);
            Write(0x00); // Positive Gamma

            dataCommandPort.State = (Command);
            Write(0xE1);
            dataCommandPort.State = (Data);
            Write(0x20);
            Write(0x20);
            Write(0x20);
            Write(0x20);
            Write(0x05);
            Write(0x00);
            Write(0x15);
            Write(0xA7);
            Write(0x3D);
            Write(0x18);
            Write(0x25);
            Write(0x2A);
            Write(0x2B);
            Write(0x2B);
            Write(0x3A); // Negative Gamma

            dataCommandPort.State = (Command);
            Write(0xB1);
            dataCommandPort.State = (Data);
            Write(0x08);
            Write(0x08); // Frame rate control 1

            dataCommandPort.State = (Command);
            Write(0xB4);
            dataCommandPort.State = (Data);
            Write(0x07);      // Display inversion

            dataCommandPort.State = (Command);
            Write(0xC0);
            dataCommandPort.State = (Data);
            Write(0x0A);
            Write(0x02); // Power control 1

            dataCommandPort.State = (Command);
            Write(0xC1);
            dataCommandPort.State = (Data);
            Write(0x02);       // Power control 2

            dataCommandPort.State = (Command);
            Write(0xC5);
            dataCommandPort.State = (Data);
            Write(0x50);
            Write(0x5B); // Vcom control 1

            dataCommandPort.State = (Command);
            Write(0xC7);
            dataCommandPort.State = (Data);
            Write(0x40);       // Vcom offset

            dataCommandPort.State = (Command);
            Write(0x2A);
            dataCommandPort.State = (Data);
            Write(0x00);
            Write(0x00);
            Write(0x00);
            Write(0x7F);
            Thread.Sleep(250); // Set column address

            dataCommandPort.State = (Command);
            Write(0x2B);
            dataCommandPort.State = (Data);
            Write(0x00);
            Write(0x00);
            Write(0x00);
            Write(0x9F);           // Set page address

            dataCommandPort.State = (Command);
            Write(0x36);
            dataCommandPort.State = (Data);
            Write(0xC0);       // Set address mode

            dataCommandPort.State = (Command);
            Write(0x29);           // Set display on
            Thread.Sleep(10);

            SetAddressWindow(0, 0, (byte)(_width - 1), (byte)(_height - 1));

            dataCommandPort.State = (Data);
        }

        private void SetAddressWindow(byte x0, byte y0, byte x1, byte y1)
        {
            dataCommandPort.State = (Command);
            Write((byte)LcdCommand.CASET);  // column addr set
            dataCommandPort.State = (Data);
            Write(0x0);
            Write(x0);   // XSTART 
            Write(0x0);
            Write(x1);   // XEND

            dataCommandPort.State = (Command);
            Write((byte)LcdCommand.RASET);  // row addr set
            dataCommandPort.State = (Data);
            Write(0x0);
            Write(y0);    // YSTART
            Write(0x0);
            Write(y1);    // YEND

            dataCommandPort.State = (Command);
            Write((byte)LcdCommand.RAMWR);  // write to RAM */
        }
    }
}