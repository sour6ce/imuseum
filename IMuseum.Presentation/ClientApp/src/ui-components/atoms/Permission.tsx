import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSession } from '../../hooks/useSession';
import { Roles } from '../../types/Roles';

interface PermissionProps {
  roles: Roles[];
  children?: React.ReactNode;
}

const areEqualProps = (
  prevProps: Readonly<PermissionProps>,
  nextProps: Readonly<PermissionProps>
): boolean => {
  if (
    prevProps.roles.length !== nextProps.roles.length
  )
    return false;

  return (
    prevProps.roles.every((role) => nextProps.roles.includes(role))
  );
};

export const Permission: React.FC<PermissionProps> = React.memo(
  ({ children, roles }) => {
    const { user } = useSession();
    const [showComponent, setShowComponent] = useState<boolean>(false);
    useEffect(() => {
      setShowComponent(true);
      return () => setShowComponent(false);
    }, []);

    if(roles.find(r=>r===Roles.Visiter)) return <>{children}</>
    let roleMatched = false;
    for (let i = 0; i < roles.length; i++) {
      if (user?.role?.includes?.(roles[i])) {
        roleMatched = true;
      }
    }

    if (roleMatched) return showComponent ? <>{children}</> : null;

    return null;
  },
  areEqualProps
);

Permission.displayName = 'Permission';

export interface RedirectionProps {
  to?: string;
}

export const Redirection: React.FC<RedirectionProps> = (props) => {
  const { to = '/' } = props;
  const navTo = useNavigate()
  useEffect(() => {
    navTo(to);
  }, [navTo, to]);
  return null;
};
