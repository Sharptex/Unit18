using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Unit18
{
    class YoutubeReceiver
    {
        public async Task<bool> ShowInfoAsync(string videoUrl)
        {
            var videos = new YoutubeClient().Videos;
            var metadate = await videos.GetAsync(videoUrl);
            Console.WriteLine(metadate.Title);
            Console.WriteLine(metadate.Description);
            return true;
        }

        public async Task<bool> DownloadAsync(string videoUrl, string outputFilePath, CancellationToken token)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            if (streamManifest.Streams.Count > 0)
            {
                var streamInfo = streamManifest.Streams[0];
                var metadate = await youtube.Videos.GetAsync(videoUrl);
                var fileName = string.Concat(metadate.Title.Split(Path.GetInvalidFileNameChars())) + "." + streamInfo.Container.Name;

                using (var progress = new InlineProgress())
                    await youtube.Videos.Streams.DownloadAsync(streamInfo, outputFilePath + fileName, progress, token);

                return true;
            } else
            {
                return false;
            }
         }
    }
}