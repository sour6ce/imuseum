import { useContext } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import AuthService  from '../../services/AuthService';
import { SessionKey, SessionStore } from '../../services/SessionStore';
import { AuthContext } from './AuthProvider';
import { Buffer } from 'buffer';

export const useAuth = () => {
  const { state, dispatch } = useContext(AuthContext);
  const navigate = useNavigate();
  const paramss = useParams();

  async function login(params: {username?:string, password?:string}) {
    const res = await AuthService.login(params?.username ?? '',params?.password ?? '');
    SessionStore.save(SessionKey.Profile, res.data);
    SessionStore.save(SessionKey.EncodedToken, Buffer.from(`${params.username}:${params.password}`).toString('base64'));
    dispatch({
      type: 'login',
      user: res.data,
    });
    const returnUrl = paramss['returnUrl'];
    if (typeof returnUrl === 'string') navigate(returnUrl);
    else navigate('/home');
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
