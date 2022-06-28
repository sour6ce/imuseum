import { useAuth } from './useAuth';

export interface UseSessionParams {
  redirectTo?: string;
}

export const useSession = (params?: UseSessionParams) => {
  const {
    state: { user },
    logout,
  } = useAuth();

  return {
    user,
    logout,
  };
};
