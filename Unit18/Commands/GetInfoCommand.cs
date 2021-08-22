using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unit18.Commands
{
    class GetInfoCommand : ICommand
    {
        private string _videoUrl;
        private YoutubeReceiver _receiver;

        public GetInfoCommand(YoutubeReceiver receiver, string videoUrl)
        {
            _receiver = receiver;
            _videoUrl = videoUrl;
        }

        public void Cancel()
        {
        }

        public async Task<bool> ExecuteAsync()
        {
            return await _receiver.ShowInfoAsync(_videoUrl);
        }
    }
}
