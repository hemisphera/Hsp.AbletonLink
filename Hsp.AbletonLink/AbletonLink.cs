using System;
using System.Runtime.InteropServices;

namespace Hsp.AbletonLink;

public sealed class AbletonLink : IDisposable
{
  private readonly IntPtr _nativeInstance;
  private const double InitialTempo = 120.0;


  [DllImport("link")]
  private static extern IntPtr CreateAbletonLink();

  [DllImport("link")]
  private static extern void DestroyAbletonLink(IntPtr ptr);

  [DllImport("link")]
  private static extern void setup(IntPtr ptr, double bpm);

  [DllImport("link")]
  private static extern void setTempo(IntPtr ptr, double bpm);

  [DllImport("link")]
  private static extern double tempo(IntPtr ptr);

  [DllImport("link")]
  private static extern void setQuantum(IntPtr ptr, double quantum);

  [DllImport("link")]
  private static extern double quantum(IntPtr ptr);

  [DllImport("link")]
  private static extern void forceBeatAtTime(IntPtr ptr, double beat);

  [DllImport("link")]
  private static extern void requestBeatAtTime(IntPtr ptr, double beat);

  [DllImport("link")]
  private static extern void enableStartStopSync(IntPtr ptr, bool bEnable);

  [DllImport("link")]
  private static extern void startPlaying(IntPtr ptr);

  [DllImport("link")]
  private static extern int numPeers(IntPtr ptr);

  [DllImport("link")]
  private static extern bool isPlaying(IntPtr ptr);

  [DllImport("link")]
  private static extern void stopPlaying(IntPtr ptr);

  [DllImport("link")]
  private static extern void update(IntPtr ptr, out double rbeat, out double rphase, out double rtempo, out double rquantum, out double rtime, out int rnumPeers);

  [DllImport("link")]
  private static extern void enable(IntPtr ptr, bool bEnable);

  [DllImport("link")]
  private static extern bool isEnabled(IntPtr ptr);


  public AbletonLink()
  {
    _nativeInstance = CreateAbletonLink();
    Setup(InitialTempo);
  }

  public void Dispose()
  {
    if (_nativeInstance == IntPtr.Zero) return;
    DestroyAbletonLink(_nativeInstance);
  }

  private void Setup(double bpm)
  {
    setup(_nativeInstance, bpm);
  }


  public double Tempo
  {
    get => tempo(_nativeInstance);
    set => setTempo(_nativeInstance, value);
  }

  public double Quantum
  {
    get => quantum(_nativeInstance);
    set => setQuantum(_nativeInstance, value);
  }

  public void ForceBeatAtTime(double beat)
  {
    forceBeatAtTime(_nativeInstance, beat);
  }


  public void RequestBeatAtTime(double beat)
  {
    requestBeatAtTime(_nativeInstance, beat);
  }


  public bool Enabled
  {
    get => isEnabled(_nativeInstance);
    set => enable(_nativeInstance, value);
  }

  public bool IsPlaying => isPlaying(_nativeInstance);

  public int NumPeers => numPeers(_nativeInstance);


  public void EnableStartStopSync(bool enable)
  {
    enableStartStopSync(_nativeInstance, enable);
  }

  public void StartPlaying()
  {
    startPlaying(_nativeInstance);
  }

  public void StopPlaying()
  {
    stopPlaying(_nativeInstance);
  }

  public LinkState GetState()
  {
    update(_nativeInstance, out var beat, out var phase, out var tempo, out var quantum, out var time, out var numPeers);
    return new LinkState
    {
      Beat = beat,
      Phase = phase,
      Tempo = tempo,
      Quantum = quantum,
      Time = time,
      NumPeers = numPeers
    };
  }
}