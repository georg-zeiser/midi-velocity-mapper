using MidiVelocityMapper.Helper;
using MidiVelocityMapper.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;

namespace Tests
{
    //TODO: more tests, better tests
    public class SettingsTest
    {
        private SettingsHelper settingsHelper;

        [SetUp]
        public void Setup()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { "settings.json", new MockFileData(@"
{
    ""MidiIn"": ""Komplete Audio 6 MIDI"",
    ""MidiOut"": ""loopMIDI Port"",
    ""SelectedCurve"": ""Yamaha P125"",
    ""Curves"": [
        {
            ""Name"": ""Yamaha P125"",
            ""Map"": [
                {
                    ""In"": 0,
                    ""Out"": 0
                },
                {
                    ""In"": 25,
                    ""Out"": 25
                },
                {
                    ""In"": 90,
                    ""Out"": 110
                },
                {
                    ""In"": 104,
                    ""Out"": 122
                },
                {
                    ""In"": 115,
                    ""Out"": 127
                }
            ]
        }
    ]
}")
                }
            });

            settingsHelper = new SettingsHelper("settings.json", fileSystem);
        }

        [Test]
        public void SerializeTest()
        {
            var settings = new Settings()
            {
                MidiIn = "Komplete Audio 6 MIDI",
                MidiOut = "loopMIDI Port",
                SelectedCurve = "Yamaha P125",
                Curves = new List<VelocityCurve>()
                {
                    new VelocityCurve()
                    {
                        Name = "Yamaha P125",
                        Map = new List<VelocityMap>()
                        {
                            new VelocityMap(0, 0),
                            new VelocityMap(25, 25),
                            new VelocityMap(90, 110),
                            new VelocityMap(104, 122),
                            new VelocityMap(115, 127)
                        }
                    }
                }
            };

            var jsonString = settingsHelper.Serialize(settings);
        }

        [Test]
        public void DeserializeTest()
        {
            var jsonString = @"
            {
                ""MidiIn"": ""Komplete Audio 6 MIDI"",
                ""MidiOut"": ""loopMIDI Port"",
                ""SelectedCurve"": ""Yamaha P125"",
                ""Curves"": [
                    {
                        ""Name"": ""Yamaha P125"",
                        ""Map"": [
                            {
                                ""In"": 0,
                                ""Out"": 0
                            },
                            {
                                ""In"": 25,
                                ""Out"": 25
                            },
                            {
                                ""In"": 90,
                                ""Out"": 110
                            },
                            {
                                ""In"": 104,
                                ""Out"": 122
                            },
                            {
                                ""In"": 115,
                                ""Out"": 127
                            }
                        ]
                    }
                ]
            }";

            var settings = settingsHelper.DeSerialize(jsonString);
        }
    }
}
