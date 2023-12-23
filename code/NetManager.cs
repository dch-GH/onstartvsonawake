using Sandbox.Network;
using System.Threading.Tasks;

namespace Sandbox;

[Title( "NetManager" )]
[Category( "Networking" )]
[Icon( "electrical_services" )]
public sealed class NetManager : Component, Component.INetworkListener
{
	/// <summary>
	/// Create a server (if we're not joining one)
	/// </summary>
	[Property] public bool StartServer { get; set; } = true;

	/// <summary>
	/// The prefab to spawn for the player to control.
	/// </summary>
	[Property] public GameObject PlayerPrefab { get; set; }

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override async Task OnLoad()
	{
		if ( Scene.IsEditor )
			return;

		if ( StartServer && !GameNetworkSystem.IsActive )
		{
			LoadingScreen.Title = "Creating Lobby";
			await Task.DelayRealtimeSeconds( 0.1f );
			GameNetworkSystem.CreateLobby();
		}
	}

	/// <summary>
	/// A client is fully connected to the server. This is called on the host.
	/// </summary>
	public void OnActive( Connection channel )
	{
		Log.Info( $"Channel Id '{channel.Id}' has joined the game" );

		if ( PlayerPrefab is null )
			return;
		//
		// Find a spawn location for this player
		//
		var startLocation = Transform.World;

		startLocation.Scale = 1;

		// Spawn this object and make the client the owner
		var player = SceneUtility.Instantiate( PlayerPrefab, startLocation );
		player.Name = $"Player - {channel.Id}";
		player.BreakFromPrefab();
		player.Network.Spawn( channel );
	}
}
