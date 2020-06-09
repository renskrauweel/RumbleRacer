using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lib.Replay;
using UnityEngine;

namespace Lib.Services
{
    public class ReplayService
    {
        public void InitReplay(string contents, List<ReplayState> _replayStates, List<float> _replayStateTimes)
        {
            foreach (var row in contents.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                if (row.Length > 1)
                {
                    ReplayState rs = new ReplayState();

                    var splittedRow = row.Split(new string[] { "--" }, StringSplitOptions.None);
                    float time = float.Parse(splittedRow[0].Trim());
                    rs.TimeMs = time;

                    string position = splittedRow[1].Trim().Substring(1, splittedRow[1].Trim().Length - 2);
                    var positions = position.Split(',');
                    Vector3 positionVector3 = new Vector3(
                        float.Parse(positions[0].Trim(), CultureInfo.InvariantCulture), 
                        float.Parse(positions[1].Trim(), CultureInfo.InvariantCulture), 
                        float.Parse(positions[2].Trim(), CultureInfo.InvariantCulture));
                    rs.Position = positionVector3;
                            
                    string rotation = splittedRow[2].Trim().Substring(1, splittedRow[2].Trim().Length - 2);
                    var rotations = rotation.Split(',');
                    Quaternion rotationQuaternion = new Quaternion(
                        float.Parse(rotations[0].Trim(), CultureInfo.InvariantCulture), 
                        float.Parse(rotations[1].Trim(), CultureInfo.InvariantCulture), 
                        float.Parse(rotations[2].Trim(), CultureInfo.InvariantCulture), 
                        float.Parse(rotations[3].Trim(), CultureInfo.InvariantCulture));
                    rs.Rotation = rotationQuaternion;
                        
                    _replayStates.Add(rs);
                    _replayStateTimes.Add(time);
                }
            }
        }

        public void UpdateReplayState(GameObject Ghost, List<ReplayState> _replayStates, List<float> _replayStateTimes)
        {
            int index = _replayStateTimes
                .Select((v, i) => new {Position = v, Index = i})
                .OrderBy(p => Math.Abs(p.Position - (Time.timeSinceLevelLoad)))
                .First().Index;

            ReplayState res = _replayStates[index];
            Ghost.transform.position = res.Position;
            Ghost.transform.rotation = res.Rotation;
        }
    }
}