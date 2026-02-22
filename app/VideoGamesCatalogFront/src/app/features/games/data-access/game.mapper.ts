import {
  VideoGameResponse,
  VideoGameAddRequest,
  VideoGameUpdateRequest,
} from '../../../core/api/generated';
import { GameModel, GameFormValue } from './game.model';

export function mapResponseToModel(response: VideoGameResponse): GameModel {
  return {
    id: response.id ?? '',
    title: response.title ?? '',
    description: response.description ?? '',
    rowVersion: response.rowVersion ?? null,
  };
}

export function mapFormToAddRequest(form: GameFormValue): VideoGameAddRequest {
  return {
    title: form.title,
    description: form.description,
  };
}

export function mapFormToUpdateRequest(
  form: GameFormValue,
  rowVersion: string | null,
): VideoGameUpdateRequest {
  return {
    title: form.title,
    description: form.description,
    rowVersion: rowVersion,
  };
}
