export interface GameModel {
  id: string;
  title: string;
  description: string;
  rowVersion: string | null;
}

export interface GameFormValue {
  title: string;
  description: string;
}
