import { Box, Breadcrumbs, Container, Link, Stack, SvgIcon } from '@mui/material';
import { useRouter } from 'next/navigation';
import ArrowUturnLeftIcon from '@heroicons/react/24/solid/ArrowUturnLeftIcon';


export const FancyBreadcrumbs = (props) => {
  const { breadcrumbsConfig } = props;
  const router = useRouter();

  return (<Box>
    <Container maxWidth="xl">
      <Stack spacing={3}>
        <Stack
          direction="row"
          justifyContent="space-between"
          spacing={4}
          sx={{ my: 2 }}
        >
          <Breadcrumbs aria-label="breadcrumb" sx={{ display: 'flex', justifyContent: 'center' }}>
            {
              breadcrumbsConfig &&
              breadcrumbsConfig.map((row, index) =>
                <Link
                  component="button"
                  underline="hover"
                  color="inherit"
                  fontSize="large"
                  onClick={() => row.url && router.push(row.url)}
                  key={index}>
                  {row.title}
                </Link>)
            }
          </Breadcrumbs>
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.back()}>
            <SvgIcon
              color="action"
              fontSize="large"
            >
              <ArrowUturnLeftIcon />
            </SvgIcon>
          </Link>
        </Stack>
      </Stack>
    </Container>
  </Box>);
}