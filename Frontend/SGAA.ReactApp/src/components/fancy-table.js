import MagnifyingGlassIcon from '@heroicons/react/24/solid/MagnifyingGlassIcon';
import {
    Box,
    Card, InputAdornment, OutlinedInput, SvgIcon, Table,
    TableBody,
    TableCell,
    TableHead,
    TablePagination,
    TableRow, TableSortLabel
} from '@mui/material';
import PropTypes from 'prop-types';
import { useCallback, useState } from 'react';
import { Scrollbar } from '/src/components/scrollbar';
import { useList } from '/src/hooks/use-lists';

export const FancyTable = (props) => {
  const { data } = props;
  const [page, setPage] = useState(0);
  const [order, setOrder] = useState('desc');
  const [orderBy, setOrderBy] = useState('id');
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [searchText, setSearchText] = useState('');

  const handleSearchChange = useCallback(
    (event) => {
      setSearchText(event.target.value);
      setPage(0);
    },
    []
  );

  const handleRequestSort = (property) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
    setPage(0);
  };

  const { list, totalCount } = useList(data, searchText.toLowerCase(), order, orderBy, page, rowsPerPage);

  const handlePageChange = (_, value) => {
    setPage(value);
  };

  const handleRowsPerPageChange =
    (event) => {
      setRowsPerPage(event.target.value);
    };

  return (
    <>
      <Card sx={{ p: 2 }}>
        <OutlinedInput
          defaultValue=""
          fullWidth
          placeholder="Buscar"
          startAdornment={(
            <InputAdornment position="start">
              <SvgIcon
                color="action"
                fontSize="small"
              >
                <MagnifyingGlassIcon />
              </SvgIcon>
            </InputAdornment>
          )}
          onChange={handleSearchChange}
        />
      </Card>
      <Card>
        <Scrollbar>
          <Box sx={{ minWidth: 800 }}>
            <Table>
              <TableHead>
                <TableRow>
                  {props.headerConfiguration.map(col =>
                  (
                    <TableCell>
                      {col.key ?
                        <TableSortLabel
                          active={orderBy === col.key}
                          direction={orderBy === col.key ? order : "asc"}
                          onClick={() => handleRequestSort(col.key)} >
                          {col.title}
                        </TableSortLabel>
                        :
                        col.title
                      }
                    </TableCell>
                  ))
                  }
                </TableRow>
              </TableHead>
              <TableBody>
                {
                  list.map((usuario) => props.tableRowGenerator(usuario))
                }
              </TableBody>
            </Table>
          </Box>
        </Scrollbar>
        <TablePagination
          component="div"
          count={totalCount}
          onPageChange={handlePageChange}
          onRowsPerPageChange={handleRowsPerPageChange}
          page={page}
          rowsPerPage={rowsPerPage}
          rowsPerPageOptions={[5, 10, 25]}
          labelRowsPerPage="Items por página"
        />
      </Card>
    </>
  );
};

FancyTable.propTypes = {
  data: PropTypes.array
};