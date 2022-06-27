export enum SessionKey {
  Profile = 'profile',
  EncodedToken = 'et',
}

export class SessionStore {
  static stringify(value: string | object) {
    return Buffer.from(JSON.stringify(value)).toString('base64');
  }

  static parse(value: string) {
    return JSON.parse(Buffer.from(value, 'base64').toString());
  }

  static load(name: SessionKey) {
    try{
      const value = localStorage.getItem(name);
      if (value === null) return undefined;
      return SessionStore.parse(value);
    }catch{
      return undefined;
    }
  }

  static save(name: SessionKey, value: string | object) {
    value = SessionStore.stringify(value);
    localStorage.setItem(name, value);
  }

  static remove(name: SessionKey) {
    localStorage.removeItem(name);
  }
}
