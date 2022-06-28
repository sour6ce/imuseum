import { Button } from "../ui-components/atoms/Button";
import { Card, CardFooter, CardHeader } from "../ui-components/molecules/Card";
import { Table } from "../ui-components/organisms/Table";
import Icon from "../ui-components/atoms/Icon";
import { Badge } from "../ui-components/atoms/Badge";
import { PaginationComponent } from "../ui-components/molecules/Pagination";
import { useRestorationsPaginated } from "../hooks/useRestorations";
import dayjs from "dayjs";
import { RestoreService } from "../services/RestoreService";
import { Popover } from "../ui-components/atoms/Popover";
import { RestorationFilterForm } from "../ui-components/organisms/RestorationFilterForm";
import { useNavigate } from "react-router-dom";
import { useSession } from "../hooks/useSession";
import { useEffect } from "react";

const Restoration = () => {
  
  const navigate = useNavigate()
  const {
    user
  } = useSession()
  useEffect(()=>{
    if(user.role !== 'Restaurator Chief'){
    navigate('/home')
  }})
  
  const {
    data,
    handleChangeFilters,
    mutate
  } = useRestorationsPaginated()
  return (
    <Card className="w-full">
      <CardHeader title="Restoration">
        <Popover
          render={({ open, close }) => (
            <div className="p-5">
              <RestorationFilterForm onSubmit={handleChangeFilters}/>
            </div>
          )}
          buttonProps={{}}
          position="right"
        >
          Filter
        </Popover>
      </CardHeader>
      <div>
        <Table
          data={data?.restorations ?? []}
          columns={[
            {
              name: "Actions",
              align: "center",
              render: (restoration) =>
                !restoration.dueDate || dayjs().isBefore(restoration.dueDate) ? (
                  <Button color="success" onClick={()=>{
                    RestoreService.endRestoration(restoration.artwork.id+"",{
                      restorationType: 'standard'
                    }).then(()=>mutate())
                  }}>
                    {<Icon family="fontawesome" name="check" />}
                  </Button>
                ) : (
                  <></>
                ),
            },
            {
              name: "Status",
              align: "center",
              render: (restoration) => (
                <Badge
                  className="uppercase font-semibold"
                  color={
                    !restoration.dueDate || dayjs().isBefore(restoration.dueDate) 
                      ? "success-light"
                      : "primary-dark"
                  }
                  textColor={
                    !restoration.dueDate || dayjs().isBefore(restoration.dueDate)
                      ? "success-dark"
                      : "gray-100"
                  }
                >
                  {
                    !restoration.dueDate || dayjs().isBefore(restoration.dueDate) ? 'In Progress' : 'Finished'}
                </Badge>
              ),
            },
            {
              name: "Artwork",
              align: "center",
              render: (restoration) => (
                <div className="felx flex-col">
                  <span className="block text-xl font-semibold">
                    {restoration.artwork.title}
                  </span>
                  <span className="uppercase text-gray-200">
                    {restoration.artwork.author}
                  </span>
                </div>
              ),
            },
            {
              name: "Start Date",
              align: "center",
              render: (restoration) => <div>{restoration.startDate}</div>,
            },
            {
              name: "End Date",
              align: "center",
              render: (restoration) => <div>{restoration.dueDate ?? '-'}</div>,
            },
          ]}
        />
      </div>
      <CardFooter>
        <PaginationComponent />
      </CardFooter>
    </Card>
  );
};

export default Restoration;
