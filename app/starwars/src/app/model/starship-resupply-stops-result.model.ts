import { StarshipResupplyStopItem } from './starship-resupply-stops-item.model';

export interface StarshipResupplyStopResult {
    distance: number;
    results: StarshipResupplyStopItem[];
}
