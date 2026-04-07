import { Card, CardContent } from "@/components/ui/card";
import { Trophy, Heart, Star, TrendingUp } from "lucide-react";
import { useRecognitions, useEmployees } from "@/hooks/useApiData";

export default function StatsCards() {
  const { data: recognitions = [] } = useRecognitions();
  const { data: employees = [] } = useEmployees();

  const totalRecognitions = recognitions.length;
  const totalPoints = recognitions.reduce((sum, r) => sum + r.points, 0);
  const uniqueRecognized = new Set(recognitions.map(r => r.toEmployeeId)).size;

  const stats = [
    { label: "Total Recognitions", value: totalRecognitions, icon: Heart, color: "text-primary" },
    { label: "Points Awarded", value: totalPoints.toLocaleString(), icon: Star, color: "text-gold" },
    { label: "Employees Recognized", value: uniqueRecognized, icon: Trophy, color: "text-success" },
    { label: "Active Employees", value: employees.length, icon: TrendingUp, color: "text-accent-foreground" },
  ];

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
      {stats.map(stat => (
        <Card key={stat.label} className="glass-card">
          <CardContent className="p-5">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-sm text-muted-foreground">{stat.label}</p>
                <p className="text-2xl font-bold mt-1">{stat.value}</p>
              </div>
              <div className={`p-3 rounded-xl bg-muted ${stat.color}`}>
                <stat.icon className="w-5 h-5" />
              </div>
            </div>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}
