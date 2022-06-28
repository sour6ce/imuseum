import React, { createContext, Dispatch, useReducer } from 'react';
import { SessionKey, SessionStore } from '../../services/SessionStore';
import { Props } from '../../types/Props';
import { User } from '../../types/User';

export const AUTH_STATE_STORAGE_KEY = 'auth' as const;

export type AuthAction =
  | {
      type: 'login';
      user: User;
    }
  | {
      type: 'logout';
    }
  | {
      type: 'load-user';
      user: User;
    }
  | {
      type: 'init';
      state: AuthState;
    };

export type AuthState = {
  user?: User;
};

export const initialAuthState: AuthState = {
  user: undefined,
};

export function AuthReducer(state: AuthState, action: AuthAction): AuthState {
  switch (action.type) {
    case 'login': {
      const { user } = action;
      return { ...state, user };
    }
    case 'logout': {
      return {
        ...state,
        user: undefined,
      };
    }
    default: {
      return state;
    }
  }
}

export const AuthContext = createContext<{
  state: AuthState;
  dispatch: Dispatch<AuthAction>;
}>({
  state: initialAuthState,
  dispatch: () => {
    return;
  },
});

const loadState = (): AuthState => {
  if (typeof window === 'undefined') return initialAuthState;
  return {
    ...initialAuthState,
    user: SessionStore.load(SessionKey.Profile),
  };
};

export const AuthProvider: React.FC<Props> = ({ children }) => {
  console.log('a')
  const [state, dispatch] = useReducer(
    AuthReducer,
    loadState() ?? initialAuthState
  );

  return (
    <AuthContext.Provider value={{ state: state, dispatch }}>
      {children}
    </AuthContext.Provider>
  );
};

export function getAuthState(): AuthState {
  return JSON.parse(localStorage.getItem(AUTH_STATE_STORAGE_KEY) as string);
}
