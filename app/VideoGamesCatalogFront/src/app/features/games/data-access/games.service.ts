import { Injectable, inject } from '@angular/core';
import { Observable, from, map } from 'rxjs';
import { Api } from '../../../core/api/generated';
import { GameModel, GameFormValue } from './game.model';
import {
  mapResponseToModel,
  mapFormToAddRequest,
  mapFormToUpdateRequest,
} from './game.mapper';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  private api = inject(Api);

  getAll(): Observable<GameModel[]> {
    return from(this.api.videoGameList()).pipe(
      map((response) => (response.data ?? []).map(mapResponseToModel)),
    );
  }

  getById(id: string): Observable<GameModel> {
    return from(this.api.videoGameDetail(id)).pipe(
      map((response) => mapResponseToModel(response.data)),
    );
  }

  add(form: GameFormValue): Observable<string> {
    return from(this.api.videoGameCreate(mapFormToAddRequest(form))).pipe(
      map((response) => response.data),
    );
  }

  update(
    id: string,
    form: GameFormValue,
    rowVersion: string | null,
  ): Observable<void> {
    return from(
      this.api.videoGameUpdate(id, mapFormToUpdateRequest(form, rowVersion)),
    ).pipe(map(() => undefined));
  }

  delete(id: string): Observable<void> {
    return from(this.api.videoGameDelete(id)).pipe(map(() => undefined));
  }
}
