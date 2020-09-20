package com.example.huynh.danhba;

import android.Manifest;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.NativeActivity;
import android.content.BroadcastReceiver;
import android.content.ContentProviderOperation;
import android.content.ContentResolver;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.Context;
import android.content.Intent;
import android.content.OperationApplicationException;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.graphics.Typeface;
import android.inputmethodservice.Keyboard;
import android.net.Uri;
import android.os.RemoteException;
import android.provider.Contacts;
import android.provider.ContactsContract;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.LoaderManager;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.ContextThemeWrapper;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import java.net.URI;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Locale;

import static android.provider.SyncStateContract.Columns.ACCOUNT_TYPE;

public class MainActivity extends AppCompatActivity {
    private DanhBaManager danhBaManager;
    private ListView listviewdanhba;
    //  private List<DanhBa> danhBaList;
    private DanhBaAdapter danhBaAdapter;
    private EditText edittim;
    private Cursor cursor;
    private Button  btthem1,btngoi,btnnhantin,btnsua,btnxoa,btnok,btnhuy,btngoiso,btncall;
    private Uri contactsListUri = ContactsContract.Contacts.CONTENT_URI;
    private String upNumber;
    private Keyboard kb;


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        checkAndRequestPermissions();
        HaiActivity hai = new HaiActivity(this);

        //checkPer();
        AnhXa();

        listviewdanhba.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view,final int position, long id) {
                final AlertDialog.Builder dialog=new AlertDialog.Builder(MainActivity.this);
                LayoutInflater inflater = getLayoutInflater();
                View dialogView = inflater.inflate(R.layout.dialog_xoasua,null);
                dialog.setView(dialogView);
                dialog.setTitle("      BẠN HÃY CHỌN CHỨC NĂNG");
                btnsua=dialogView.findViewById(R.id.btn_sua1);
                btnxoa=dialogView.findViewById(R.id.btn_xoa1);
                final AlertDialog adialog = dialog.create();
                btnsua.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        final Dialog dialogsua = new Dialog(MainActivity.this);
                        dialog.setTitle("     THAY ĐỔI THÔNG TIN LIÊN HỆ");
                        dialogsua.setContentView(R.layout.dialog_sua);
                        btnok=dialogsua.findViewById(R.id.btn_ok);
                       final EditText edttensua=dialogsua.findViewById(R.id.ed_tensua);
                       final EditText edtsdtsua=dialogsua.findViewById(R.id.ed_sdtsua);
                        edttensua.setText(danhBaAdapter.getItem(position).getTen().toString());
                        edtsdtsua.setText(danhBaAdapter.getItem(position).getSdt().toString());
                        btnhuy=dialogsua.findViewById(R.id.btn_huy1);
                        btnok.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                String ten,tenUp,soUp;
                                tenUp=edttensua.getText().toString();
                                soUp=edtsdtsua.getText().toString();
                                ten=danhBaAdapter.getItem(position).getTen().toString();

                                //so=danhBaAdapter.getItem(position).getSdt().toString();
                                xoacontact(MainActivity.this,ten);
                                addcontact(tenUp,soUp);

                               // Log.d("data", "onClick: "+tenUp+"...."+soUp);//da set dc ten + sdt moi

                                setadapter();
                                dialogsua.cancel();
                                adialog.cancel();
                            }
                        });

                        btnhuy.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                dialogsua.cancel();
                            }
                        });
                        dialogsua.show();
                    }
                });
                btnxoa.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        xoacontact(MainActivity.this,danhBaAdapter.getItem(position).getTen().toString());
                        Toast.makeText(getBaseContext(), "Đã Xóa Thành Công Số Liên Lạc", Toast.LENGTH_SHORT).show();
                        setadapter();
                        adialog.cancel();
                    }
                });
                adialog.show();
                return true;
            }
        });
        listviewdanhba.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, final int position, long id) {
                final AlertDialog.Builder dialog=new AlertDialog.Builder(MainActivity.this);
                LayoutInflater inflater = getLayoutInflater();
                View dialogView = inflater.inflate(R.layout.custom_dialog,null);
                dialog.setView(dialogView);
                dialog.setTitle("     BẠN HÃY CHỌN CHỨC NĂNG");
                dialog.show();
                btngoi = dialogView.findViewById(R.id.btn_goi);
                btnnhantin = dialogView.findViewById(R.id.btn_nhantin);
                final AlertDialog adialog = dialog.create();
                btngoi.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        intentGoi(position);
                    }
                });
                btnnhantin.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        intentNhantin(position);
                    }
                });
                return;
            }
        });
        btthem1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDialog̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣();

            }
        });
        btncall.setOnClickListener(new View.OnClickListener() { // Sai ở đây nè
            // goi activity ban phim
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, Call.class);
                startActivity(intent);
            }
        });

        setadapter();


        cursor = managedQuery(contactsListUri, null, null, null, null);
        edittim.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                String text = edittim.getText().toString().toLowerCase(Locale.getDefault());
                danhBaAdapter.filter(text);
            }
        });


    }

    public void setadapter() {
    danhBaManager = new DanhBaManager(MainActivity.this);
    danhBaAdapter = new DanhBaAdapter(MainActivity.this, R.layout.dong_danh_ba, danhBaManager.getMdanhBaList());
    listviewdanhba.setAdapter(danhBaAdapter);
    danhBaAdapter.notifyDataSetChanged();
    }

    private void showDialog̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣̣() {
        final AlertDialog.Builder dialog=new AlertDialog.Builder(MainActivity.this);
        LayoutInflater inflater = getLayoutInflater();
        View dialogView = inflater.inflate(R.layout.layout_dialog,null);
        dialog.setView(dialogView);
        dialog.setTitle("      BẠN HÃY NHẬP THÔNG TIN  ");
        final EditText edit_ten = dialogView.findViewById(R.id.ed_ten);
        final EditText edit_sdt = dialogView.findViewById(R.id.ed_sdt);
        Button them = (Button) dialogView.findViewById(R.id.btn_them);
        Button huy = (Button) dialogView.findViewById(R.id.btn_huy);
        final AlertDialog adialog = dialog.create();
        them.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String ten = edit_ten.getText().toString();
                String sdt = edit_sdt.getText().toString();
                addcontact(edit_ten.getText().toString(), sdt);
                setadapter();
                adialog.cancel();
            }
        });
        huy.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                adialog.cancel();
            }
        });
        adialog.show();
    }

    public void intentGoi(int i)
    {
        Intent intentCall=new Intent();
        intentCall.setAction(Intent.ACTION_CALL);
        intentCall.setData(Uri.parse("tel:"+danhBaAdapter.getItem(i).getSdt()));
        startActivity(intentCall);
    }
    public void intentNhantin(int i)
    {
        Intent intentCall=new Intent();
        intentCall.setAction(Intent.ACTION_VIEW);
        intentCall.setData(Uri.parse("sms:"+danhBaAdapter.getItem(i).getSdt()));
        startActivity(intentCall);
    }
    private void addcontact(String name, String phone) {
        ArrayList<ContentProviderOperation> ops = new ArrayList<ContentProviderOperation>();
        int rawContactID = ops.size();
        ops.add(ContentProviderOperation.newInsert(ContactsContract.RawContacts.CONTENT_URI).withValue(ContactsContract.RawContacts.ACCOUNT_TYPE, null).withValue(ContactsContract.RawContacts.ACCOUNT_NAME, null).build());
        ops.add(ContentProviderOperation.newInsert(ContactsContract.Data.CONTENT_URI).withValueBackReference(ContactsContract.Data.RAW_CONTACT_ID, rawContactID).withValue(ContactsContract.Data.MIMETYPE, ContactsContract.CommonDataKinds.StructuredName.CONTENT_ITEM_TYPE).withValue(ContactsContract.CommonDataKinds.StructuredName.DISPLAY_NAME, name).build());
        ops.add(ContentProviderOperation.newInsert(ContactsContract.Data.CONTENT_URI).withValueBackReference(ContactsContract.Data.RAW_CONTACT_ID, rawContactID).withValue(ContactsContract.Data.MIMETYPE, ContactsContract.CommonDataKinds.Phone.CONTENT_ITEM_TYPE).withValue(ContactsContract.CommonDataKinds.Phone.NUMBER, phone).withValue(ContactsContract.CommonDataKinds.Phone.TYPE, ContactsContract.CommonDataKinds.Phone.TYPE_MOBILE).build());
        try {
            getContentResolver().applyBatch(ContactsContract.AUTHORITY, ops);
            Toast.makeText(getBaseContext(), "Đã Thêm Thành Công Số Liên Lạc", Toast.LENGTH_SHORT).show();
        } catch (RemoteException e) {
            e.printStackTrace();
        } catch (OperationApplicationException e) {
            e.printStackTrace();
        }
        danhBaManager.getMdanhBaList();
        //ops.clear();
        //danhBaList.clear();
        //listviewdanhba.invalidate();
        danhBaAdapter.notifyDataSetChanged();
    }

    private void xoacontact(Context context, String name) {
        ContentResolver cr = getContentResolver();
        String where = ContactsContract.Data.DISPLAY_NAME + " = ? ";
        String[] params = new String[]{name};

        ArrayList<ContentProviderOperation> ops = new ArrayList<ContentProviderOperation>();
        ops.add(ContentProviderOperation.newDelete(ContactsContract.RawContacts.CONTENT_URI)
                .withSelection(where, params)
                .build());
        try {
            cr.applyBatch(ContactsContract.AUTHORITY, ops);
        } catch (Exception e) {

        }
    }

    public void AnhXa() {

        listviewdanhba = (ListView) findViewById(R.id.listview);

        btthem1 = findViewById(R.id.btnthem1);
        edittim = findViewById(R.id.editsearch);
        btncall= findViewById(R.id.btncall);
        //danhBaList=new ArrayList<>();

    }
    private void checkAndRequestPermissions() {
        String[] permissions = new String[]{
                Manifest.permission.CALL_PHONE,
                Manifest.permission.SEND_SMS,Manifest.permission.READ_CONTACTS,
                Manifest.permission.WRITE_CONTACTS
        };
        List<String> listPermissionsNeeded = new ArrayList<>();
        for (String permission : permissions) {
            if (ContextCompat.checkSelfPermission(this, permission) != PackageManager.PERMISSION_GRANTED) {
                listPermissionsNeeded.add(permission);
            }
        }
        if (!listPermissionsNeeded.isEmpty()) {
            ActivityCompat.requestPermissions(this, listPermissionsNeeded.toArray(new String[listPermissionsNeeded.size()]), 1);
        }
    }




}

