include Makefile.helpers
modname = Vagabond
dependencies = Proficiencies SynchronizedWorldObjects TinyHelper IronCoin TinyQuests

assemble:
	# common for all mods
	rm -f -r public
	@make dllsinto TARGET=$(modname) --no-print-directory
	
	@make basefolders
	
	cp -u resources/textures/tame_beast_status_effect.png      public/$(sideloaderpath)/Texture2D/tamedStatusEffect.png
	cp -u resources/textures/tame_beast_status_effect.png      public/$(sideloaderpath)/Texture2D/animalCompanionStatusEffect.png
	cp -u resources/textures/tame_beast_status_effect.png      public/$(sideloaderpath)/Texture2D/tamingStatusEffect.png
	
	@make item NAME="IronCoin" FILENAME="IronCoin" PREPATH="../IronCoin/"
	@make skill NAME="AnimalCompanion" FILENAME="tame_beast"
	@make skill NAME="TameBeast" FILENAME="tame_beast"
	@make skill NAME="ThrowSand" FILENAME="throw_salt"
	@make skill NAME="CommandBeast" FILENAME="command_beast"
	@make skill NAME="PrecisionStrike" FILENAME="precision_strike"
	@make skill NAME="Forager" FILENAME="forager"
	@make skill NAME="CarefulMaintenance" FILENAME="careful_maintenance"
	
forceinstall:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)

play:
	(make install && cd .. && make play)
