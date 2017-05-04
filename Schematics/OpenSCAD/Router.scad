module router(a,b)
{
// _device();
// first: opened, second: angle?
 _device();
antenna(a,b);
// antenna(0,0);
// antenna(1,0);
// antenna(0,1);
// antenna(0,2);
//antenna(0,3);
}

module dcPlug()
{
        difference()
    {
    color("black")translate([100,9+5*14+8,15-2])rotate([0,180,0])cube(size=[10, 5, 8]);
    color("dimgray")translate([100,9+5*14+8+2.5,15-2-2.5])rotate([0,90,0])cylinder(r=2, h=10, center=true, $fn=20);
    }
    color("gray")translate([100,9+5*14+8+2.5,15-2-2.5])rotate([0,-90,0])cylinder(r=0.5, h=10, $fn=20);
    
}

module _device()
{
    difference()
    {

        color("Gainsboro")linear_extrude(height=25)
        {
            translate([2.5,2.5])
                offset(2.5)
                    square(95);
        }

        //color("red")
        for (i=[0:3])
            translate([101,9+i*14,15])rotate([90,180,-90])rj45slot();
        translate([101,9+4*14+2,15])rotate([90,180,-90])rj45slot();
    translate([101,9+5*14+8,15-2])rotate([0,180,0])cube(size=[10, 5, 8]);
    }
    dcPlug();

}

module rj45slot()
{
    rj45points=[[0,0],[12,0],[12,7],[9,7],[9,9],[8,9],[8,10],[4,10],[4,9],[3,9],[3,7],[0,7]];
    linear_extrude(12)polygon(rj45points);
}


module antenna(rotX,rotY)
{
    // rotate([0,0,0])
    translate([20,0,13])
    color("grey")
    {
        rotate([90,0,0])linear_extrude(height=20)circle(d=13); // trzon
        translate([0,-13,0])rotate([rotX*90,0,0])rotate([0,rotY*90,0])
        translate([0,0,-7])
        {
            // translate([0,0,0])
            cylinder(d=13, h=18);
            translate([0,0,18])cylinder(d1=13, d2=5, h=20);
            translate([0,0,18+20])cylinder(d=5, h=84, $fn=20);
            // linear_extrude(height=18)circle(d=13);
            // linear_extrude(height=20)circle(d=9);
            // linear_extrude(height=84)circle(d=5);
        }
    }
}