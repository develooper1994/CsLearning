using Microsoft.CognitiveServices.Speech;

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CallSqlFunction
{
    public static class Speech
    {
        public static void OldOnlyWindowsSpeak(string text, bool wait = false)
        {
            ExecutePSCommand(
                $@"Add-Type -AssemblyName System.speech;
                $speak = New-Object System.Speech.Synthesis.SpeechSynthesizer;
                $speak.Speak(""{text}"");");

            void ExecutePSCommand(string PScommand)
            {
                string path = Path.GetTempPath() + Guid.NewGuid() + ".ps1";

                // make sure to be using System.Text
                using StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
                sw.Write(PScommand);

                ProcessStartInfo start = new ProcessStartInfo()
                {
                    FileName = @"powershell.exe",
                    LoadUserProfile = false,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"-executionpolicy bypass -File {path}",
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process process = Process.Start(start);

                if (wait)
                    process.WaitForExit();
            }
        }

        public static async Task RecognizeSpeechAsync(string SubscriptionKey = "YourSubscriptionKey",
            string ServiceRegion = "YourServiceRegion")
        {
            /*
             I can't take subscription key for now.
             */
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key // and service region (e.g., "westus").
            var config = SpeechConfig.FromSubscription(SubscriptionKey, ServiceRegion);

            // Creates a speech recognizer.
            using var recognizer = new SpeechRecognizer(config);
            Console.WriteLine("Say something...");

            // Starts speech recognition, and returns after a single utterance is recognized. The end of a
            // single utterance is determined by listening for silence at the end or until a maximum of 15
            // seconds of audio is processed.  The task returns the recognition text as result.
            // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
            // shot recognition like command or query.
            // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
            var result = await recognizer.RecognizeOnceAsync();

            // Checks result.
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"We recognized: {result.Text}");
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                }
            }
        }
    }





}
