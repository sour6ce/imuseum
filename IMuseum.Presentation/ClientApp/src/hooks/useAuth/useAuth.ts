import { useRouter } from 'next/router';
import { useContext } from 'react';
import AuthService  from '../../services/AuthService';
import { SessionKey, SessionStore } from '../../services/SessionStore';
import { AuthContext } from './AuthProvider';

export const useAuth = () => {
  const { state, dispatch } = useContext(AuthContext);
  const router = useRouter();

  async function login(params: {username?:string, password?:string}) {
    const res = await AuthService.login(params?.username ?? '',params?.password ?? '');
    SessionStore.save(SessionKey.Profile, res.data.profile);
    SessionStore.save(SessionKey.EncodedToken, Buffer.from(`${params.username}:${params.password}`).toString('base64'));
    dispatch({
      type: 'login',
      user: res.data.profile,
    });
    const returnUrl = router.query['returnUrl'];
    if (typeof returnUrl === 'string') router.push(returnUrl);
    else router.push('/home');
  }

  async function logout() {
    SessionStore.remove(SessionKey.Profile);
    SessionStore.remove(SessionKey.EncodedToken);
    dispatch({ type: 'logout' });
  }

  return {
    state,
    dispatch,
    login,
    logout,
  };
};
