import { useRouter } from 'next/router';
import { registrarUsuario } from '/src/api/administrador';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuarioCrearForm } from '/src/sections/usuario/usuario-crear-form';

const Page = () => {
  const router = useRouter();

  const onUsuarioCreated = (result) => {
    if (result)
      router.push('/administrador/usuario');
  };

  const breadcrumbsConfig = [
    { url: '/', title: 'Inicio' },
    { url: '/administrador/usuario', title: 'Usuarios' },
    { url: '/administrador/usuario/crear', title: 'Crear' }
  ];

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={<UsuarioCrearForm handleSubmit={registrarUsuario} handleConfirmationChange={onUsuarioCreated} includeAdminRol={true} />}
      entityName={'Usuario'}
      breadcrumbsConfig={breadcrumbsConfig}>
    </FancyFormPage>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
