import classNames from "classnames";




export interface TableColumn<T>{
  name: string;
  render: (data: T)=>React.ReactNode;
  minWidth?: number;
  align?: 'left' | 'right' | 'center';
}

export interface TableProps<T>{
  columns: TableColumn<T>[];
  data: T[];
  onRow?: (data:T)=>void
}


export const Table = <T extends unknown>(props:TableProps<T>)=>{
  return(
    <table className="w-full border-collapse  table-auto ">
      <thead className="bg-side rounded-b-md bg-opacity-50">
        {props.columns.map(c=>(
          <th key={c.name} className={classNames('text-left p-2 px-5 font-semibold uppercase border border-gray-900',{
            [`text-${c.align}`]: c.align,
            'text-left': !c.align
          })}>{c.name}</th>
        ))}
      </thead>
      <tbody>
        {props.data && props.data.map((d,index)=>(
          <tr className="hover:bg-gray-950 hover:bg-opacity-50 cursor-pointer border border-gray-900" key={index} onClick={()=>props?.onRow?.(d)}>
            {props.columns.map(c=>(
              <td key={c.name} style={{minWidth: c.minWidth}} className={classNames('p-2 px-5 border border-gray-900',{
                [`text-${c.align}`]: c.align,
                'text-left': !c.align
              })}>{c.render(d)}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  )
}
