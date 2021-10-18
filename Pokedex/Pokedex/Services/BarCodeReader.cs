//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Honeywell.AIDC.CrossPlatform;
//using static Honeywell.AIDC.CrossPlatform.BarcodeReaderBase;

//namespace Pokedex.Services
//{
//    internal class BarCodeReader : IBarCodeReader
//    {
//        public event EventHandler<BarcodeDataArgs> BarcodeDataHasBeenReceived;

//        private BarcodeReader _barCodeReader;

//        public BarCodeReader()
//        {
//            // _logger = logProvider.GetLogFor<BarCodeReaderProvider>();
//        }

//        public async Task CloseAsync()
//        {
//            if (_barCodeReader != null)
//            {
//                // _logger.Trace("Close Reader");
//                // Turn off the software trigger.
//                _barCodeReader.BarcodeDataReady -= OnBarCodeDataReceived;
//                BarcodeDataHasBeenReceived = null;

//                var result = await _barCodeReader.CloseAsync();
//                if (result.Code != Result.Codes.SUCCESS)
//                {
//                    // _logger.Error($"CloseAsync failed. Code: {result.Code} | Message : {result.Message}");
//                }
//            }
//        }

//        public void ResetSubscriptions()
//        {
//            BarcodeDataHasBeenReceived = null;
//        }

//        private static readonly Random _random = new Random();

//        public async Task OpenAsync()
//        {
//            if (_barCodeReader is null)
//            {
//                await InitializeAsync();
//            }

//            var value = _random.Next(1000);
//            await Task.Delay(value);

//            if (!_barCodeReader.IsReaderOpened)
//            {
//                // _logger.Info("Open Reader");
//                var result = await _barCodeReader.OpenAsync();
//                if (result.Code == Result.Codes.SUCCESS || result.Code == Result.Codes.READER_ALREADY_OPENED)
//                {
//                    await DefineBarCodeReaderSettingsAsync();
//                    _barCodeReader.BarcodeDataReady += OnBarCodeDataReceived;
//                }
//                else
//                {
//                    // _logger.Error($"OpenAsync failed. Code: {result.Code} | Message : {result.Message}");
//                }
//            }
//        }

//        private DateTime _latestScanDate = DateTime.Now;
//        private static readonly TimeSpan _timer = TimeSpan.FromMilliseconds(300);

//        private void OnBarCodeDataReceived(object sender, BarcodeDataArgs e)
//        {
//            if (DateTime.Now - _timer >= _latestScanDate)
//            {
//                _latestScanDate = DateTime.Now;
//                BarcodeDataHasBeenReceived?.Invoke(sender, e);
//            }
//        }

//        private async Task InitializeAsync()
//        {
//            // _logger.Info($"Barcode InitializeAsync");
//            try
//            {
//                var readers = await BarcodeReader.GetConnectedBarcodeReaders();
//                _barCodeReader = new BarcodeReader(readers.First().ScannerName);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                // _logger.Error($"Initialize failed. | Message : {ex.Message}");
//            }
//        }

//        private async Task DefineBarCodeReaderSettingsAsync()
//        {
//            try
//            {
//                var settings = new Dictionary<string, object>()
//        {
//                    { _barCodeReader.SettingKeys.TriggerScanMode, _barCodeReader.SettingValues.TriggerScanMode_OneShot },
//                    { _barCodeReader.SettingKeys.Interleaved25Enabled, true },
//                    { _barCodeReader.SettingKeys.Interleaved25MaximumLength, 10 },
//                };

//                var result = await _barCodeReader.SetAsync(settings);
//                if (result.Code != Result.Codes.SUCCESS)
//                {
//                    // _logger.Error($"Symbology settings failed. Code: {result.Code} | Message : {result.Message}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                // _logger.ErrorException($"Symbology settings failed. Code: Exception: {ex.Message} ", ex);
//            }
//        }
//    }
//}