import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { Box, Button, Container, Stack, SvgIcon, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useEffect, useMemo, useState } from 'react';
import { getUsuarios } from '/src/api/administrador';
import { AuthGuard } from '/src/guards/auth-guard';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuariosSearch } from '/src/sections/usuario/usuario-search';
import { UsuariosTable } from '/src/sections/usuario/usuario-table';
import { applyPagination } from '/src/utils/apply-pagination';


const useUsuarios = (filteredData, page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(filteredData, page, rowsPerPage);
    },
    [filteredData, page, rowsPerPage]
  );
};

const Page = () => {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [searchText, setSearchText] = useState('');
  const lcSearchText = searchText.toLowerCase();
  const filteredData = data.filter((item) =>
    Object.values(item).some(
      field => field.toString().toLowerCase().includes(lcSearchText)
    )
  );

  const usuarios = useUsuarios(filteredData, page, rowsPerPage);

  const handleSearchChange = useCallback(
    (event) => {
      setSearchText(event.target.value);
      setPage(0);
    },
    []
  );

  const handlePageChange = useCallback(
    (event, value) => {
      setPage(value);
    },
    []
  );

  const handleRowsPerPageChange = useCallback(
    (event) => {
      setRowsPerPage(event.target.value);
    },
    []
  );

  useEffect(() => {
    getUsuarios()
      .then((response) => {
        setData(response);
      });
  }, []);

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          Usuarios
        </title>
      </Head>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          py: 8
        }}
      >
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Stack spacing={1}>
                <Typography variant="h4">
                  Usuarios
                </Typography>
              </Stack>
              <div>
                <Button
                  startIcon={(
                    <SvgIcon fontSize="small">
                      <PlusIcon />
                    </SvgIcon>
                  )}
                  variant="contained"
                >
                  Crear Usuario Administrador
                </Button>
              </div>
            </Stack>
            <UsuariosSearch onSearchChange={handleSearchChange} />
            <UsuariosTable
              count={filteredData.length}
              items={usuarios}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              page={page}
              rowsPerPage={rowsPerPage}
            />
          </Stack>
        </Container>
      </Box>
    </AuthGuard>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
