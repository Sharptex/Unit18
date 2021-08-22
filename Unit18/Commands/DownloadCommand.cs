using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Unit18.Commands
{
    class DownloadCommand : ICommand
    {
        private YoutubeReceiver _reciever;
        private string _videoUrl;
        private string _outputFilePath;
        private CancellationTokenSource _source;

        public DownloadCommand(YoutubeReceiver reciever, string videoUrl, string outputFilePath)
        {
            _reciever = reciever;
            _videoUrl = videoUrl;
            _outputFilePath = outputFilePath;
            _source = new CancellationTokenSource();
        }

        public void Cancel()
        {
            _source.Cancel();
        }

        public async Task<bool> ExecuteAsync()
        {
            return await _reciever.DownloadAsync(_videoUrl, _outputFilePath, _source.Token);
        }
    }
}
