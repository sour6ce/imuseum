import classNames from 'classnames';
import { useMemo, useState } from 'react';

export interface Pagination {
  page?: number;
  pageSize?: number;
  /**
   * Max count of pages to show in pagination
   * @default 6
   */
  maxPages?: number;
  total?: number;
}

export interface PaginationProps extends Pagination {
  className?: string;
  onChange?: (pagination: Pagination) => void;
}

export const initialPaginationState: Pagination = {
  page: 0,
  pageSize: 5,
  maxPages: 10,
};

export const PaginationComponent = (props: PaginationProps) => {
  const [paginationState, setPaginationState] = useState<Pagination>({
    ...initialPaginationState,
  });

  const pagination = useMemo(
    () => ({
      page: props.page ?? paginationState.page,
      pageSize: props.pageSize ?? paginationState.pageSize,
      maxPages: props.maxPages ?? paginationState.maxPages,
      total: props.total ?? paginationState.total,
    }),
    [props, paginationState]
  );

  const pagesCount = useMemo(
    () => Math.ceil((pagination.total ?? 0) / (pagination.pageSize ?? 1)),
    [pagination]
  );
  const pageOffset = useMemo(
    () => Math.floor((pagination.maxPages ?? 10) / 2),
    [pagination]
  );

  const pages = useMemo(() => {
    const p: number[] = [];

    const start = pagination.page;

    let end =
      (pagination.page ?? 0) +
      pageOffset +
      Math.max(0, (pagination.maxPages ?? 0) - (start ?? 0) - pageOffset);
    end = Math.min(end, pagesCount - 1);

    const preStart = Math.max(0, (start ?? 0) - (pagination.maxPages ?? 0) + (end - (start ?? 0)));
    const preEnd = Math.max(0, (start ?? 0));

    for (let i = preStart; i < preEnd; i++) {
      p.push(i);
    }

    for (let i = (start ?? 0); i <= end; i++) {
      p.push(i);
    }

    return p;
  }, [pagination, pageOffset, pagesCount]);

  const handleSelectPage = (index: number) => {
    handlePaginationChange({
      ...pagination,
      page: index,
    });
  };

  const handleNextPage = () => {
    handlePaginationChange({
      ...pagination,
      page: (pagination.page ?? 0) + 1,
    });
  };

  const handlePreviousPage = () => {
    handlePaginationChange({
      ...pagination,
      page: (pagination.page ?? 0) - 1,
    });
  };

  const handlePaginationChange = (pagination: Pagination) => {
    setPaginationState(pagination);
    if (props.onChange) props.onChange(pagination);
  };

  return (
    <ul className={classNames('pagination', props.className)}>
      <li
        className={classNames('page-item previous', {
          disabled: pagination.page === 0,
        })}
      >
        <button onClick={handlePreviousPage} className="page-link">
          <i className="previous"></i>
        </button>
      </li>
      {pages.map((p) => (
        <li
          className={classNames('page-item', {
            active: pagination.page === p,
          })}
          key={p}
        >
          <button onClick={() => handleSelectPage(p)} className="page-link">
            {p + 1}
          </button>
        </li>
      ))}
      <li
        className={classNames('page-item next', {
          disabled: pagination.page === pagesCount - 1,
        })}
      >
        <button onClick={handleNextPage} className="page-link">
          <i className="next"></i>
        </button>
      </li>
    </ul>
  );
};
