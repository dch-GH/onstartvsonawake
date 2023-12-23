using Sandbox;
using Sandbox.Network;

public sealed class MyComponent : Component
{
	protected override void OnAwake()
	{
		Log.Info( $"OnAwake -- IsHost: {GameNetworkSystem.IsHost}, IsProxy: {IsProxy}, OwnerId: {Network.OwnerId}, IsOwner: {Network.IsOwner}" );
	}

	protected override void OnStart()
	{
		Log.Info( $"OnStart --- IsHost: {GameNetworkSystem.IsHost}, IsProxy: {IsProxy}, OwnerId: {Network.OwnerId}, IsOwner: {Network.IsOwner}" );
	}
}
