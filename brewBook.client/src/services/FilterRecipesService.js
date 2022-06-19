import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class FilterRecipesService
{
	async get() {
		const res = await api.get('api/filterrecipes')
		logger.log('filter recipes',res.data)
		AppState.filterRecipes = res.data
	}
}

export const filterRecipesService = new FilterRecipesService();