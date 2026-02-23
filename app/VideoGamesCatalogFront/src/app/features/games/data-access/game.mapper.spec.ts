import {
  mapResponseToModel,
  mapFormToAddRequest,
  mapFormToUpdateRequest,
} from './game.mapper';
import { VideoGameResponse } from '../../../core/api/generated';
import { GameFormValue } from './game.model';

describe('Game Mappers', () => {
  describe('mapResponseToModel', () => {
    it('should map complete response to model', () => {
      const response: VideoGameResponse = {
        id: '123',
        title: 'Test Game',
        description: 'Test Description',
        rowVersion: 'abc123',
      };

      const result = mapResponseToModel(response);

      expect(result).toEqual({
        id: '123',
        title: 'Test Game',
        description: 'Test Description',
        rowVersion: 'abc123',
      });
    });

    it('should handle null/undefined fields with defaults', () => {
      const response: VideoGameResponse = {
        id: undefined,
        title: null,
        description: undefined,
        rowVersion: null,
      };

      const result = mapResponseToModel(response);

      expect(result).toEqual({
        id: '',
        title: '',
        description: '',
        rowVersion: null,
      });
    });

    it('should handle empty response', () => {
      const response: VideoGameResponse = {};

      const result = mapResponseToModel(response);

      expect(result.id).toBe('');
      expect(result.title).toBe('');
      expect(result.description).toBe('');
      expect(result.rowVersion).toBeNull();
    });
  });

  describe('mapFormToAddRequest', () => {
    it('should map form value to add request', () => {
      const form: GameFormValue = {
        title: 'New Game',
        description: 'New Description',
      };

      const result = mapFormToAddRequest(form);

      expect(result).toEqual({
        title: 'New Game',
        description: 'New Description',
      });
    });

    it('should preserve empty strings', () => {
      const form: GameFormValue = {
        title: '',
        description: '',
      };

      const result = mapFormToAddRequest(form);

      expect(result.title).toBe('');
      expect(result.description).toBe('');
    });
  });

  describe('mapFormToUpdateRequest', () => {
    it('should map form value with rowVersion to update request', () => {
      const form: GameFormValue = {
        title: 'Updated Game',
        description: 'Updated Description',
      };

      const result = mapFormToUpdateRequest(form, 'version-1');

      expect(result).toEqual({
        title: 'Updated Game',
        description: 'Updated Description',
        rowVersion: 'version-1',
      });
    });

    it('should handle null rowVersion', () => {
      const form: GameFormValue = {
        title: 'Updated Game',
        description: 'Updated Description',
      };

      const result = mapFormToUpdateRequest(form, null);

      expect(result.rowVersion).toBeNull();
    });
  });
});
