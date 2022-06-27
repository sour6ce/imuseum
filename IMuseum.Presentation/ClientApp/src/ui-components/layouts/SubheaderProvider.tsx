import { useCallback } from 'react';
import { createContext, useState } from 'react';
import { Props } from '../../types/Props';

interface contextProps {
  breadcrumbs: breadcrumbType[];
  addBreadcrumb: (path: string, name: string, available?: boolean) => void;
  addManyBreadcrumbs: (breads: breadcrumbType[]) => void;
  deleteBreadcrumb: (path: string, name: string, available?: boolean) => void;
  deleteManyBreadcrumbs: (breads: breadcrumbType[]) => void;
}

export type breadcrumbType = {
  path: string;
  name: string;
  available?: boolean;
};

export const SubheaderProvider: React.FC<Props> = ({ children }) => {
  const [breadcrumbs, setBreadcrumbs] = useState<breadcrumbType[]>([]);

  const addBreadcrumb = useCallback(
    (path: string, name: string, available = false) => {
      const bread: breadcrumbType = {
        path,
        name,
        available,
      };
      setBreadcrumbs((prevstate) => {
        if (prevstate.find((breadcrumb) => breadcrumb.path === bread.path))
          return prevstate;
        return [...prevstate, bread];
      });
    },
    []
  );

  const addManyBreadcrumbs = useCallback((breads: breadcrumbType[]) => {
    const newBreadLinks = breads.map((b) => b.path);
    setBreadcrumbs((prevstate) => {
      return [
        ...prevstate.filter((b) => !newBreadLinks.includes(b.path)),
        ...breads,
      ];
    });
  }, []);

  const deleteBreadcrumb = useCallback(
    (path: string, name: string, available = false) => {
      const bread: breadcrumbType = {
        path,
        name,
        available,
      };
      setBreadcrumbs((prevstate) => [
        ...prevstate.filter((breadcrumb) => breadcrumb.path !== bread.path),
      ]);
    },
    []
  );
  const deleteManyBreadcrumbs = useCallback((breads: breadcrumbType[]) => {
    const newBreadLinks = breads.map((b) => b.path);
    setBreadcrumbs((prevstate) => {
      return [...prevstate.filter((b) => !newBreadLinks.includes(b.path))];
    });
  }, []);

  return (
    <SubheaderContext.Provider
      value={{
        addBreadcrumb,
        addManyBreadcrumbs,
        breadcrumbs,
        deleteBreadcrumb,
        deleteManyBreadcrumbs,
      }}
    >
      {children}
    </SubheaderContext.Provider>
  );
};

export const SubheaderContext = createContext<contextProps>({
  breadcrumbs: [],
  addBreadcrumb: (path: string, name: string, available?: boolean) => {
    return;
  },
  addManyBreadcrumbs: (breads: breadcrumbType[]) => {
    return;
  },
  deleteBreadcrumb: (path: string, name: string, available?: boolean) => {
    return;
  },
  deleteManyBreadcrumbs: (breads: breadcrumbType[]) => {
    return;
  },
});
