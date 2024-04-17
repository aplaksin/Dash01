mergeInto(LibraryManager.library, {
	Hello: function (){
		console.log("1111");
	},
	IsPlayerAuthorisedExternal: function (){
		var isAuth = player.getMode() === "lite" ? '0' : '1';
		//console.log('isAuth ' + isAuth);
		MyGameInstance.SendMessage("Yandex", "SetIsPlayerAuthorised", isAuth);
	},
	
	GetPlayerBestScoreExternal: function (){
		//console.log('GetPlayerBestScoreExternal');
		lb.getLeaderboardPlayerEntry('bestscore')
		  .then(res => MyGameInstance.SendMessage("Yandex", "GetPlayerBestScore", res.score + ''))
		  .catch(err => {
			console.error('CRASH GetPlayerBestScore: ', err.message);
		  });

		
	},
	
	
	SetPlayerBestScoreExternal: function (score){

		ysdk.getLeaderboards()
		.then(lb => {
    // Без extraData
			lb.setLeaderboardScore('bestscore', score);
		});

	},
	
	ShowAdFullScreenExternal: function(){
		ysdk.adv.showFullscreenAdv({
		callbacks: {
			onClose: function(wasShown) {
			  MyGameInstance.SendMessage("Yandex", "AdFullScreenClosed");
			},
		   onError: function(error) {
			  MyGameInstance.SendMessage("Yandex", "AdFullScreenClosed");
			}
		}
	})
	},
});