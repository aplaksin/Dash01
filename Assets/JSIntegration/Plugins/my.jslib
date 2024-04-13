mergeInto(LibraryManager.library, {
	Hello: function (){
		console.log("1111");
	},
	IsPlayerAuthorisedExternal: function (){
		var isAuth = player.getMode() === "lite" ? 0 : 1;
		console.log('isAuth ' + isAuth);
		MyGameInstance.SendMessage("Yandex", "SetIsPlayerAuthorised", isAuth);
	},
	
	GetPlayerBestScoreExternal: function (){
		ysdk.getLeaderboards()
		  .then(lb => lb.getLeaderboardPlayerEntry('score'))
		  .then(res => {
			  MyGameInstance.SendMessage("Yandex", "GetPlayerBestScore", res.score);
			  console.log('res.score ' + res.score);
			  })
		  .catch(err => {
			if (err.code === 'LEADERBOARD_PLAYER_NOT_PRESENT') {
			  console.log('no score');
			}
		  });
		
	},
	SetPlayerBestScoreExternal: function (score){
		ysdk.getLeaderboards()
		  .then(lb => {
			// Без extraData
			console.log(score);
			lb.setLeaderboardScore('score', score);
			
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