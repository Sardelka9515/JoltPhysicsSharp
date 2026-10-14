// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using static JoltPhysicsSharp.JoltApi;

namespace JoltPhysicsSharp;

public sealed class VehicleTransmission : NativeObject
{
    internal VehicleTransmission(nint handle, bool owns = true)
        : base(handle, owns)
    {
    }

    internal static VehicleTransmission? GetObject(nint handle) => GetOrAddObject(handle, h => new VehicleTransmission(h, false));

    public void SetMode(TransmissionMode mode)
    {
        JPH_VehicleTransmission_SetMode(Handle, mode);
    }

    public void Set(int currentGear, float clutchFriction)
    {
        JPH_VehicleTransmission_Set(Handle, currentGear, clutchFriction);
    }

    public void Update(float deltaTime, float currentRPM, float forwardInput, bool canShiftUp)
    {
        JPH_VehicleTransmission_Update(Handle, deltaTime, currentRPM, forwardInput, canShiftUp);
    }

    /// <summary>
    /// Changes a forward gear's ratio while the vehicle drives, as a continuously variable transmission does; Jolt reads it each step.
    /// </summary>
    /// <remarks>
    /// Jolt's VehicleTransmission derives from VehicleTransmissionSettings, whose ratios it drives with, and joltc's handles are
    /// pointer casts, so the settings' setter writes the live ratio.
    /// </remarks>
    public void SetGearRatio(int index, float ratio)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, JPH_VehicleTransmissionSettings_GetGearRatioCount(Handle));
        JPH_VehicleTransmissionSettings_SetGearRatio(Handle, index, ratio);
    }

    public int CurrentGear => JPH_VehicleTransmission_GetCurrentGear(Handle);
    public float ClutchFriction => JPH_VehicleTransmission_GetClutchFriction(Handle);
    public bool IsSwitchingGear => JPH_VehicleTransmission_IsSwitchingGear(Handle);
    public float CurrentRatio => JPH_VehicleTransmission_GetCurrentRatio(Handle);

    public bool AllowSleep => JPH_VehicleTransmission_AllowSleep(Handle);
}
