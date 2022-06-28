import { Button } from "../ui-components/atoms/Button";
import { Card, CardFooter, CardHeader } from "../ui-components/molecules/Card";
import { Table } from "../ui-components/organisms/Table";
import Icon from "../ui-components/atoms/Icon";
import { Badge } from "../ui-components/atoms/Badge";
import { PaginationComponent } from "../ui-components/molecules/Pagination";

const Restoration = () => {
  const restorations = [
    {
      onClick: () => {},
      status: "in progress",
      artwork: "The Creation",
      author: "Miguel Angel",
      type: "standard",
      startDate: "4/5/12",
      endDate: "-",
    },
    {
      onClick: () => {},
      status: "finished",
      artwork: "The Creation",
      author: "Miguel Angel",
      type: "standard",
      startDate: "4/5/12",
      endDate: "-",
    },
  ];
  return (
    <Card className="w-full">
      <CardHeader title="Restoration" />
      <div>
        <Table
          data={restorations}
          columns={[
            {
              name: "Actions",
              align: "center",
              render: (restoration) =>
                restoration.status === "in progress" ? (
                  <Button color="success">
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
                    restoration.status === "in progress"
                      ? "success-light"
                      : "primary-dark"
                  }
                  textColor={
                    restoration.status === "in progress"
                      ? "success-dark"
                      : "gray-100"
                  }
                >
                  {restoration.status}
                </Badge>
              ),
            },
            {
              name: "Artwork",
              align: "center",
              render: (restoration) => (
                <div className="felx flex-col">
                  <span className="block text-xl font-semibold">
                    {restoration.artwork}
                  </span>
                  <span className="uppercase text-gray-200">
                    {restoration.author}
                  </span>
                </div>
              ),
            },
            {
              name: "Type",
              align: "center",
              render: (restoration) => <div>{restoration.type}</div>,
            },
            {
              name: "Start Date",
              align: "center",
              render: (restoration) => <div>{restoration.startDate}</div>,
            },
            {
              name: "End Date",
              align: "center",
              render: (restoration) => <div>{restoration.endDate}</div>,
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
