import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class EspressoRecipesService
{
	async get() {
		const res = await api.get('api/espressorecipes')
		logger.log('espresso recipes', res.data)
		AppState.espressoRecipes = res.data
	}
}

export const espressoRecipesService = new EspressoRecipesService();